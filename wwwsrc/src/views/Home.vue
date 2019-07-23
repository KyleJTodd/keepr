<template>
  <div class="home">
    <navbar></navbar>
    <h1>{{user.username}}'s Dashboard</h1>
    <div class="row">
      <div class="col-4">
        <button @click="hideForm = !hideForm">Create New Vault</button>
        <form v-if="hideForm" @submit.prevent="createVault">
          <input type="text" placeholder="New Vault Name">
          <input type="text" placeholder="New Vault Description">
          <br>
          <button class="btn btn-info">Create</button>
        </form>
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
        hideForm: false
      }
    },
    computed: {
      user() {
        return this.$store.state.user;
      }
    },
    methods: {
      logout() {
        this.$store.dispatch("logout");
      }
    },
    components: {
      Navbar
    }
  };
</script>