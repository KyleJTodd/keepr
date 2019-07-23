<template>
  <div class="row">
    <div class="col-12">
      <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <a class="navbar-brand">Keepr</a>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavDropdown"
          aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNavDropdown">
          <ul class="navbar-nav">
            <li class="nav-item active">
              <a class="nav-link">
                <router-link :to="{name: 'home'}">Dashboard</router-link><span class="sr-only">(current)</span>
              </a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="#">
                <router-link :to="{name: 'keeps'}">Explore</router-link>
              </a>
            </li>

            <li class="nav-item">
              <a class="nav-link" v-if="user.id" @click="logout">Logout</a>
              <router-link v-else :to="{name: 'login'}">Login</router-link>
            </li>
          </ul>
        </div>
      </nav>
    </div>
  </div>



</template>




<script>
  export default {
    name: "Navbar",

    methods: {
      logout() {
        this.$store.dispatch("logout");
      }
    },
    computed: {
      user() {
        return this.$store.state.user;
      }
    }
  }

</script>

<style>
  .navbar {
    height: 10vh;
  }
</style>