<template>
  <div>
    <navbar></navbar>
    <h1>{{vault.name}}</h1>
    <div v-for="vaultKeep in vaultKeeps">
      <div class="card" style="width: 18rem;">
        <img v-bind:src="vaultKeep.img" class="card-img-top" alt="Image">
        <div class="card-body">
          <h5 class="card-title">{{vaultKeep.name}}</h5>
          <p class="card-text">{{vaultKeep.description}}
          </p>
          <a href="#" class="btn btn-danger" @click="deleteVaultKeep(vaultKeep.id)">Remove?</a>
        </div>
      </div>


    </div>














  </div>
</template>






<script>
  import Navbar from '../components/Navbar.vue'
  export default {
    name: "vault",
    props: ["vaultId"],

    computed: {
      vault() {
        return this.$store.state.vault
      },
      persist() {
        return this.$route.params.id
      },
      vaultKeeps() {
        return this.$store.state.vaultKeeps
      }
    },
    mounted() {

      this.$store.dispatch("getVaultKeeps", this.vaultId)
    },
    methods: {
      deleteVaultKeep(vaultKeep) {
        let data = {
          keepId: vaultKeep,
          vaultId: this.vaultId
        }
        this.$store.dispatch('deleteVaultKeep', data)
      }
    },

    components: {
      Navbar
    }
  }


</script>