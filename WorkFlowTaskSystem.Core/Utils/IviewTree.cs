﻿using System.Collections.Generic;
using System.Linq;

namespace WorkFlowTaskSystem.Core
{
    /// <summary>
    /// IView tree组件
    /// </summary>
   public class IviewTree
    {
        
        public string Id { get; set; }
        public string Title { get; set; }
        public bool Expand { get; set; }
        
        public bool Checked{get; set; }
        public bool Selected { get; set; }
        public object Data { get; set; }

        public List<IviewTree> Children { get; set; }
        /// <summary>
        /// 由线性结构递归生成树形结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="all">源</param>
        /// <param name="currentId">当前id</param>
        /// <returns></returns>
        public static List<IviewTree> RecursiveQueries<T> (List<T> all, string currentId = null) where T : ITree
        {
            List<IviewTree> datatree = null;
            if (string.IsNullOrEmpty(currentId))
            {
                datatree = all.Where(u => u.ParentId is null|| u.ParentId=="-1"||u.ParentId=="").Select(e => new IviewTree() { Title = e.Name, Id = e.Id, Data = e }).ToList();
            }
            else {
                datatree = all.Where(u => u.ParentId == currentId).Select(e => new IviewTree() { Title = e.Name, Id = e.Id, Data = e }).ToList();
            }
             
            if (datatree == null || datatree.Count <= 0)
            {
                return new List<IviewTree>();
            }
            foreach (var dto in datatree)
            {
                dto.Children = new List<IviewTree>();
                dto.Children.AddRange(RecursiveQueries(all, dto.Id));
            }
            return datatree;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="all"></param>
        /// <param name="currentId"></param>
        /// <param name="selectIds">默认选中</param>
        /// <returns></returns>
        public static List<IviewTree> RecursiveQueries<T>(List<T> all, List<string> selectIds, string currentId = null) where T : ITree
        {
            List<IviewTree> datatree = null;
            if (string.IsNullOrEmpty(currentId))
            {
                datatree = all.Where(u => u.ParentId is null || u.ParentId == "-1" || u.ParentId == "").Select(e => new IviewTree() { Title = e.Name, Id = e.Id, Checked = selectIds.Contains(e.Id) }).ToList();
            }
            else
            {
                datatree = all.Where(u => u.ParentId == currentId).Select(e => new IviewTree() { Title = e.Name, Id = e.Id, Checked = selectIds.Contains(e.Id) }).ToList();
            }
            
            if (datatree == null || datatree.Count <= 0)
            {
                return new List<IviewTree>();
            }
            foreach (var dto in datatree)
            {
                dto.Children = new List<IviewTree>();
                dto.Children.AddRange(RecursiveQueries(all, selectIds, dto.Id));
            }
            return datatree;
        }

        public static List<IviewTree> LinearQueries<T>(List<T> all,List<string> selectIds)
            where T : ILinear
        {
            List<IviewTree> datatree = all.Select(e => new IviewTree() { Title = e.Name, Id = e.Id, Checked = selectIds.Contains(e.Id) }).ToList();
            if (datatree == null || datatree.Count <= 0)
            {
                return new List<IviewTree>();
            }
            return datatree;
        }
    }
    public class AsyncIviewTree
    {

        public string Id { get; set; }
        public string Title { get; set; }
        public bool Expand { get; set; }

        public bool Checked { get; set; }
        public bool Selected { get; set; }
        public object Data { get; set; }
        public bool Loading { get; set; }
        public List<IviewTree> Children { get; set; }
    
    }
    /// <summary>
    /// 树形关系
    /// </summary>
    public interface ITree
    {
        string Id { get; set; }
        string ParentId { get; set; }
        string Name { get; set; }
    }
    /// <summary>
    /// 线性关系
    /// </summary>
    public interface ILinear
    {
        string Id { get; set; }
        string Name { get; set; }

    }
}
