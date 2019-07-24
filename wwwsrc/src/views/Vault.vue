<template>
  <div>
    <div>{{vault.name}}</div>


  </div>




</template>






<script>
  export default {
    name: "vault",
    props: [],

    computed: {
      vault() {
        return this.$store.state.vault
      },
      persist() {
        return this.$route.params.id
      }
    },
    2
  }


</script>