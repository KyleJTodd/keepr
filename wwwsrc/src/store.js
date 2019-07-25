import Vue from 'vue'
import Vuex from 'vuex'
import Axios from 'axios'
import router from './router'
import AuthService from './AuthService'

Vue.use(Vuex)

let baseUrl = location.host.includes('localhost') ? '//localhost:5000/' : '/'

let api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 3000,
  withCredentials: true
})

export default new Vuex.Store({
  state: {
    user: {},
    keeps: [],
    vaults: [],
    vault: [],
    vaultKeeps: [],
    userKeeps: []
  },
  mutations: {
    setUser(state, user) {
      state.user = user
    },
    resetState(state) {
      //clear the entire state object of user data
      state.user = {}
    },
    setKeeps(state, keeps) {
      state.keeps = keeps
    },
    setVaults(state, vaults) {
      state.vaults = vaults
    },
    setVault(state, vault) {
      state.vault = vault
    },
    setVaultKeeps(state, vaultKeeps) {
      state.vaultKeeps = vaultKeeps
    },
    setUserKeeps(state, userKeeps) {
      state.userKeeps = userKeeps
    }
  },
  actions: {
    async register({ commit, dispatch }, creds) {
      try {
        let user = await AuthService.Register(creds)
        commit('setUser', user)
        router.push({ name: "home" })
      } catch (e) {
        console.warn(e.message)
      }
    },
    async login({ commit, dispatch }, creds) {
      try {
        let user = await AuthService.Login(creds)
        commit('setUser', user)
        router.push({ name: "home" })
      } catch (e) {
        console.warn(e.message)
      }
    },
    async logout({ commit, dispatch }) {
      try {
        let success = await AuthService.Logout()
        if (!success) { }
        commit('resetState')
        router.push({ name: "login" })
      } catch (e) {
        console.warn(e.message)
      }
    },
    async getAllKeeps({ commit, dispatch }) {
      await api.get('keeps')
        .then(res => {
          commit('setKeeps', res.data)
        })
    },
    async createVault({ commit, dispatch }, newVault) {
      await api.post('vaults', newVault)
        .then(res => {
          dispatch('getVaults', res.data)
        })
    },
    async getVaults({ commit, dispatch }) {
      await api.get('vaults')
        .then(res => {
          commit('setVaults', res.data)
        })
    },
    async deleteVault({ commit, dispatch }, vaultId) {

      await api.delete('vaults/' + vaultId)
        .then(res => {
          dispatch('getVaults', res.data)
        })
    },
    async createKeep({ commit, dispatch }, newKeep) {

      await api.post('keeps', newKeep)
        .then(res => {
          dispatch('getAllKeeps')
        })
    },
    async deleteKeep({ commit, dispatch }, keepId) {
      await api.delete('keeps/' + keepId)
        .then(res => {
          dispatch('getAllKeeps')
          dispatch('getUserKeeps')
        })

    },
    async addToVault({ commit, dispatch }, data) {
      await api.post('vaultkeeps', data)
        .then(res => {
          dispatch('getAllKeeps')
        })
    },
    async goToVault({ commit, dispatch }, vaultId) {

      await api.get('vaults/' + vaultId)
        .then(res => {
          commit('setVault', res.data)

          router.push({ name: "vault", params: { vaultId: vaultId } })
        })
    },
    async getVaultKeeps({ commit, dispatch }, vaultId) {
      await api.get('vaultkeeps/' + vaultId)
        .then(res => {
          commit('setVaultKeeps', res.data)
        })
    },
    async getUserKeeps({ commit, dispatch }) {
      await api.get('keeps/' + 'user')
        .then(res => {
          dispatch('getAllKeeps')
          commit('setUserKeeps', res.data)
        })
    },
    async deleteVaultKeep({ commit, dispatch }, data) {

      await api.put('vaultkeeps', data)
        .then(res => {
          dispatch('getVaultKeeps', data.vaultId)
        })
    }
  }
})
