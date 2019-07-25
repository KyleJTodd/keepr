<template>
  <div class="row">
    <h1>Welcome to Keepr. Where you can find and keep what interests you.</h1>
    <div v-for="keep in keeps">
      <div class="card" style="width: 18rem;">
        <img v-bind:src="keep.img" class="card-img-top" alt="Image">
        <div class="card-body">
          <h5 class="card-title">{{keep.name}}</h5>
          <p class="card-text">{{keep.description}}
          </p>

        </div>
      </div>
    </div>



  </div>
</template>




<script>
  export default {
    name: "AllKeeps",
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
    }

  }










</script>