﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Cells;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using WorkFlowTaskSystem.Application;
using WorkFlowTaskSystem.Application.CheckTables;
using WorkFlowTaskSystem.Application.Documents.BridgeConstants;
using WorkFlowTaskSystem.Application.Documents.CableConstants;
using WorkFlowTaskSystem.Application.Documents.ReportResults;
using WorkFlowTaskSystem.Application.Documents.ReportResults.Dto;
using WorkFlowTaskSystem.Controllers;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.Reports;
using WorkFlowTaskSystem.Core.ViewModel;
using WorkFlowTaskSystem.Web.Core.Models;

namespace WorkFlowTaskSystem.Web.Core.Controllers
{
    public class FileHelperController : WorkFlowTaskSystemControllerBase
    {
        private IHostingEnvironment _hostingEnvironment;
        private ICheckTableAppService _checkTableAppService;
        private ICableConstantAppService _cableConstantAppService;
        private IBridgeConstantAppService _bridgeConstantAppService;
        private IReportResultAppService _reportResultAppService;
    public FileHelperController(IHostingEnvironment hostingEnvironment, ICheckTableAppService checkTableAppService, ICableConstantAppService cableConstantAppService, IBridgeConstantAppService bridgeConstantAppService, IReportResultAppService reportResultAppService)
    {
      _hostingEnvironment = hostingEnvironment;
      _checkTableAppService = checkTableAppService;
      _cableConstantAppService = cableConstantAppService;
      _bridgeConstantAppService = bridgeConstantAppService;
      _reportResultAppService = reportResultAppService;
    }

    #region 文件上传、下载、删除、预览
    //[RequestSizeLimit(100_000_000)] //最大100m左右
    [DisableRequestSizeLimit]  //或者取消大小的限制
    public IActionResult Upload(string methods, string randomnumber)
    {
      var files = Request.Form.Files;

      long size = files.Sum(f => f.Length);

      string webRootPath = _hostingEnvironment.WebRootPath;

      string contentRootPath = _hostingEnvironment.ContentRootPath;
      List<string> filenames = new List<string>();
      List<string> realnames = new List<string>();
            string filename = "";
      foreach (var formFile in files)

      {

        if (formFile.Length > 0)
        {
          filename = formFile.FileName;
          string fileExt = Path.GetExtension(formFile.FileName); //文件扩展名，不含“.”
          long fileSize = formFile.Length; //获得文件大小，以字节为单位
          string newFileName = System.Guid.NewGuid().ToString() + fileExt; //随机生成新的文件名
          var filePath = webRootPath + "/upload/";
          if (!Directory.Exists(filePath))
          {
            Directory.CreateDirectory(filePath);
          }
          using (var stream = new FileStream(filePath + newFileName, FileMode.Create))

          {
            formFile.CopyTo(stream);
          }
          filenames.Add(newFileName);
                    realnames.Add(filename);
        }
      }
            if (methods == "Cable") {
                try
                {
                    _checkTableAppService.InsertCableLayingDetails(randomnumber, webRootPath + "/upload/" + filenames[0]);
                    return Ok(new { filenames, realnames, count = files.Count, size });
                }
                catch (Exception e) {

                    return StatusCode(500, new { name = filename + e.Message });
                }
            }
            else {
                return Ok(new { filenames, realnames, count = files.Count, size });
            }


      
    }
    public IActionResult DownLoad(string file)

    {
      string webRootPath = _hostingEnvironment.WebRootPath;
      var addrUrl = webRootPath + "/upload/" + file;

      var stream = System.IO.File.OpenRead(addrUrl);

      string fileExt = Path.GetExtension(file);

      //获取文件的ContentType

      var provider = new FileExtensionContentTypeProvider();

      var memi = provider.Mappings[fileExt];

      return File(stream, memi, Path.GetFileName(addrUrl));

    }

    public IActionResult DeleteFile(string file)
    {
      string webRootPath = _hostingEnvironment.WebRootPath;
      var addrUrl = webRootPath + "/upload/" + file;
      if (System.IO.File.Exists(addrUrl))
      {
        //删除文件
        System.IO.File.Delete(addrUrl);
      }
      return Ok(new { file });
    }

    /// <summary>
    /// 预览pdf报表
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public IActionResult Preview(string file)
    {
      string webRootPath = _hostingEnvironment.WebRootPath;
      var addrUrl = webRootPath + "/reports/" + file;

      var stream = System.IO.File.OpenRead(addrUrl);

      string fileExt = Path.GetExtension(file);

      //获取文件的ContentType

      var provider = new FileExtensionContentTypeProvider();

      var memi = provider.Mappings[fileExt];

      return File(stream, memi, Path.GetFileName(addrUrl));
    } 
    #endregion
    /// <summary>
    /// 校验
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public IActionResult Check([FromBody]CheckEntityView entity)
    {
      try
      {
        DeleteFile(entity.NumberNo + ".xlsx");
        string webRootPath = _hostingEnvironment.WebRootPath;
        
        var designlist = _checkTableAppService.GetCableLayingDetailsListByNumberNo(entity.NumberNo);
        List<string> list = new List<string>();
        Workbook wb = new Workbook(webRootPath + "/upload/" + entity.RealityTable);
        var sheet = wb.Worksheets[0];
        var maxCol = sheet.Cells.MaxColumn + 1;
        for (int i = 1; i < sheet.Cells.MaxRow + 1; i++)
        {
          var cablecode = sheet.Cells[i, 2].Value.ToString().Trim();
          var flag = designlist.Exists(u => u.CableCode == cablecode);
          sheet.Cells[i, maxCol].HtmlString = (!flag ? "<span style='color:red;'>和设计不一致</span>" : "<span style='color:blue;'>和设计一致</span>");
          list.Add(cablecode);
        }
        var maxRow = sheet.Cells.MaxRow + 2;
        foreach (var detailse in designlist)
        {
          var flag = list.Exists(u => u == detailse.CableCode);
          if (!flag)
          {
            Aspose.Cells.Style style = sheet.Cells[maxRow, 2].GetStyle();
            //设置背景颜色
            style.ForegroundColor = System.Drawing.Color.Red;
            style.Pattern = BackgroundType.Solid;
            sheet.Cells[maxRow, 2].SetStyle(style);
            sheet.Cells[maxRow, 2].PutValue(detailse.CableCode);
            sheet.Cells[maxRow, 8].PutValue(detailse.Start.RoomCode);
            sheet.Cells[maxRow, 9].PutValue(detailse.Start.SystemName);
            sheet.Cells[maxRow, 10].PutValue(detailse.Start.EquitName);
            sheet.Cells[maxRow, 11].PutValue(detailse.Start.EquitCode);
            sheet.Cells[maxRow, 12].PutValue(detailse.End.RoomCode);
            sheet.Cells[maxRow, 13].PutValue(detailse.End.SystemName);
            sheet.Cells[maxRow, 14].PutValue(detailse.End.EquitName);
            sheet.Cells[maxRow, 15].PutValue(detailse.End.EquitCode);
            sheet.Cells[maxRow, 16].PutValue(detailse.SafePassage);
            sheet.Cells[maxRow, 17].PutValue(detailse.PressureVesselCode);
            sheet.Cells[maxRow, 18].PutValue(detailse.CabinCode);
            sheet.Cells[maxRow, 19].PutValue(detailse.PipeCode);
            sheet.Cells[maxRow, 20].PutValue(detailse.Version);
            sheet.Cells[maxRow, 21].PutValue(detailse.Specification);
            sheet.Cells[maxRow, 22].PutValue(detailse.Length);
            sheet.Cells[maxRow, 23].PutValue(detailse.PipeSpecification);
            sheet.Cells[maxRow, 24].PutValue(detailse.PipeLength);
            sheet.Cells[maxRow, 25].PutValue(detailse.CablePath);
            sheet.Cells[maxRow, 26].PutValue(detailse.Other);
                        maxRow += 1;
          }
        }
        string fileExt = "." + entity.RealityTable.Split('.')[1];
        //获取文件的ContentType
        var provider = new FileExtensionContentTypeProvider();
        var memi = provider.Mappings[fileExt];
        MemoryStream stream = new MemoryStream();
        wb.Save(stream, Aspose.Cells.SaveFormat.Xlsx);
        stream.Position = 0;
        return File(stream, memi);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        //throw;
        return StatusCode(500, e.Message);
      }
      

    }

        /// <summary>
        ///  转换格式
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IActionResult TransferFormat([FromBody]CheckEntityView entity)
        {
            DeleteFile(entity.NumberNo + ".xlsx");
            string file = _hostingEnvironment.WebRootPath + "/upload/" + entity.NumberNo + ".xlsx";
            var designlist = _checkTableAppService.GetCableLayingDetailsListByNumberNo(entity.NumberNo);
            NPOIstaticHelper.DesignSource(file, designlist);
            return DownLoad(entity.NumberNo + ".xlsx");
        }
        public IActionResult TransferFormat1([FromBody]CheckEntityView entity)
        {
            DeleteFile(entity.NumberNo + ".xlsx");
            string file = _hostingEnvironment.WebRootPath + "/upload/" + entity.NumberNo + ".xlsx";
            var designlist = _checkTableAppService.GetCableLayingDetailsListByNumberNo(entity.NumberNo);
            NPOIstaticHelper.DesignSourceCollect(file, designlist);
            return DownLoad(entity.NumberNo + ".xlsx");
        }
        /// <summary>
        /// 计算容积率、载重量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IActionResult Calculate([FromBody]CheckEntityView entity)
    {
      Task.Run(() =>                                          //异步开始执行
      {
          
         try
        {
          var addrUrl = _hostingEnvironment.WebRootPath + "/upload/" + entity.RealityTable;
          _checkTableAppService.InsertCableSummarizedBill(entity.NumberNo, addrUrl);
          var list = _checkTableAppService.GetBridgeInstancesListByNumberNo(entity.NumberNo);
          string file = _hostingEnvironment.WebRootPath + "/upload/" + entity.NumberNo + ".xlsx";
          NPOIHelper nPOI = new NPOIHelper(file);
              List<ReportView> reports = new List<ReportView>();
          foreach (var bridgeInstancese in list)
          {
            var reportView = _checkTableAppService.SummationCable(entity.NumberNo, bridgeInstancese.BridgeCode);
                  reportView.BridgeCode = bridgeInstancese.BridgeCode;
                  reportView.PassageType = bridgeInstancese.PassageType;
                  reportView.SectionalArea = double.Parse(bridgeInstancese.SectionalArea); 
                  reportView.WeightLimit = double.Parse(bridgeInstancese.WeightLimit);
                  reportView.PlotRatioLimit = double.Parse(bridgeInstancese.PlotRatioLimit);
                  reportView.PlotRatio = Math.Round(reportView.CableArea / reportView.SectionalArea, 2);
                  reports.Add(reportView);
               
           }
              var bridgelist=_bridgeConstantAppService.GetListToNotIn(list.Select(u => u.BridgeCode).ToList());
              foreach (var item in bridgelist.GroupBy(u => u.BridgeCode)) {
                  ReportView reportView = new ReportView();
                  reportView.CableArea = 0;
                  reportView.CableWeight = 0;
                  reportView.CableNumber = 0;
                  reportView.CableCodes = "";
                  reportView.BridgeCode = item.Key;
                  reportView.PassageType =string.Join("/",item.Select(u=>u.PassageType));
                  reportView.SectionalArea = double.Parse(item.First().SectionalArea);
                  reportView.WeightLimit = double.Parse(item.First().WeightLimit);
                  reportView.PlotRatioLimit = double.Parse(item.First().PlotRatioLimit);
                  reportView.PlotRatio = Math.Round(reportView.CableArea / reportView.SectionalArea, 2);
                  reports.Add(reportView);
              }
              nPOI.Insert(reports.OrderByDescending(u=>u.PlotRatio).ToList());
          nPOI.Save();
          _reportResultAppService.Create(new ReportResultDto() {  Name = "生成计算容积率报告成功", Url = entity.NumberNo + ".xlsx", Description = entity.NumberNo });
          }
        catch (Exception e)
        {
          Console.WriteLine(e);
          _reportResultAppService.Create(new ReportResultDto() { Name = "生成报告失败", Url = entity.NumberNo + ".xlsx", Description = entity.NumberNo });
        }
        
      });

      return Ok(entity.NumberNo);
    }
   

  }
}
