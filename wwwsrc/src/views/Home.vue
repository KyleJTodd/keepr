<template>
  <div class="home">
    <navbar></navbar>
    <h1>{{user.username}}'s Dashboard</h1>
    <div class="row">
      <div class="col-12">
        <button @click="hideForm = !hideForm">Create New Vault</button>
        <form v-if="hideForm" @submit.prevent="createVault(); hideForm = !hideForm">
          <input type="text" v-model="newVault.name" placeholder="New Vault Name">
          <br>
          <input type="text" v-model="newVault.description" placeholder="New Vault Description">
          <br>
          <button class="btn btn-info">Create</button>
        </form>
      </div>
    </div>
    <div class="row">
      <div class="offset-1 col-10">
        <div v-for="vault in vaults" v-bind:vault="vault">
          <div class="jumbotron">
            <h1 class="display-12">{{vault.name}}</h1>
            <p class="lead">{{vault.description}}</p>
            <hr class="my-12">
            <a class="btn btn-primary" href="#" role="button" @click="goToVault(vault.id)">View Vault</a>
            <br>
            <br>
            <p><button class="btn btn-danger" @click="deleteVault(vault.id)">Delete</button></p>
          </div>
        </div>
      </div>
    </div>
    <div class="row">

      <h1 class="col-12 text-center">All of your created keeps</h1>
      <div class="row">
        <div v-for="userKeep in userKeeps">
          <div class="card" style="width: 18rem;">
            <img v-bind:src="userKeep.img" class="card-img-top" alt="Image">
            <div class="card-body">
              <h5 class="card-title">{{userKeep.name}}</h5>
              <p class="card-text">{{userKeep.description}}
              </p>
              <a href="#" class="btn btn-danger" @click="deleteKeep(userKeep.id)">Delete?</a>
            </div>
          </div>
        </div>
      </div>


    </div>
  </div>






  </div>
</template>

<script>
  import Navbar from "../components/Navbar.vue"
  export default {
    name: "home",
    data() {
      return {
        hideForm: false,
        newVault: {
          name: "",
          description: ""
        }
      }
    },
    mounted() {
      this.$store.dispatch('getVaults')
      this.$store.dispatch('getUserKeeps')
    },

    computed: {
      user() {
        return this.$store.state.user;
      },
      vaults() {
        return this.$store.state.vaults;
      },
      userKeeps() {
        return this.$store.state.userKeeps;
      },
      keeps() {
        return this.$store.state.keeps;
      }
    },
    methods: {
      logout() {
        this.$store.dispatch("logout");
      },
      createVault() {
        this.$store.dispatch("createVault", this.newVault)
      },
      deleteVault(vaultId) {
        this.$store.dispatch('deleteVault', vaultId)
      },
      goToVault(vaultId) {
        this.$store.dispatch('goToVault', vaultId)
      },
      deleteKeep(keepId) {

        this.$store.dispatch('deleteKeep', keepId)
      },


    },
    components: {
      Navbar
    }
  }

</script>