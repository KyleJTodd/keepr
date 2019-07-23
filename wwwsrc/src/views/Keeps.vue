<template>
  <div class="row">
    <div class="col-12">
      <navbar></navbar>
    </div>
    <div class="row">
      <div class="col-12">
        <h1 class="text-center">Find new things that interest you!</h1>
      </div>
      <div class="row">
        <div v-for="keep in keeps">
          <div class="card" style="width: 18rem;">
            <img v-bind:src="keep.img" class="card-img-top" alt="Image">
            <div class="card-body">
              <h5 class="card-title">{{keep.name}}</h5>
              <p class="card-text">{{keep.description}}
              </p>
              <a class="btn btn-primary">Add to Vault</a>
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
    name: "keeps",
    methods: {

    },
    mounted() {
      this.$store.dispatch("getAllKeeps");
    },
    computed: {
      keeps() {
        return (
          this.$store.state.keeps
        )
      }
    },
    components: {
      Navbar
    }

  }
</script>