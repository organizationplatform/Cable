import Vue from 'vue';
import Vuex from 'vuex';

import app from './modules/app';
import user from './modules/user';
import role from './modules/role';
import permission from './modules/permission';
import organization from './modules/organization';
import session from './modules/session';
import document from './modules/document';
import filemanager from './modules/filemanager';
import cableconstant from './modules/cableconstant';
import bridgeconstant from './modules/bridgeconstant';
import reportresult from './modules/reportresult';
import weightconstant from './modules/weightconstant';
import plotratio from './modules/plotratio';

Vue.use(Vuex);

const store = new Vuex.Store({
    state: {
        //
    },
    mutations: {
        //
    },
    actions: {

    },
    modules: {
        app,
        user,
        role,
        permission,
        organization,
        session,
        document,
        filemanager,
        cableconstant,
        bridgeconstant,
        reportresult,
        weightconstant,
        plotratio
    }
});

export default store;
