<template>
  <div class="row">
    <div class="col-12">
      <navbar></navbar>
      <div class="col-12">
        <form @submit.prevent="createKeep">
          <input class="form-control" type="text" placeholder="Name" v-model="newKeep.name">
          <input class="form-control" type="text" placeholder="Description" v-model="newKeep.description">
          <input class="form-control" type="text" placeholder="Image URL" v-model="newKeep.img">
          <label>Would you like this post to be private?</label>
          <br>
          <label class="switch">
            <input type="checkbox" v-model="newKeep.isPrivate" :value=true>

            <span class="slider round"></span>
          </label>
          <button type="submit" class="btn btn-primary form-control">Create a Keep</button>
        </form>
      </div>
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
              <form @submit.prevent="addToVault(keep.id)">
                <select v-model="selected">
                  <option v-for="vault in vaults" :value="vault.id" placeholder="Vaults">{{vault.name}}</option>
                </select>
                <br>
                <button class="btn btn-primary">Add To Vault</button>
              </form>
              <a v-if="user.id == keep.userId" class="btn btn-danger" @click="deleteKeep(keep.id)">Delete</a>
              <a v-else></a>
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
    data() {
      return {
        newKeep: {
          name: "",
          description: "",
          img: "",
          isPrivate: false
        },
        selected: ""
      }
    },
    methods: {
      createKeep() {
        let data = this.newKeep
        this.$store.dispatch('createKeep', data)
      },
      deleteKeep(keepId) {
        this.$store.dispatch('deleteKeep', keepId)
      },
      addToVault(keepId) {

        let data = {
          vaultId: this.selected,
          keepId: keepId
        }
        this.$store.dispatch('addToVault', data)
      }
    },
    mounted() {
      this.$store.dispatch("getAllKeeps");
      this.$store.dispatch("getVaults");
    },
    computed: {
      keeps() {
        return (
          this.$store.state.keeps
        )
      },
      user() {
        return (
          this.$store.state.user
        )
      },
      vaults() {
        return (
          this.$store.state.vaults
        )
      }
    },

    components: {
      Navbar
    }

  }
</script>



<style>
  /* The switch - the box around the slider */
  .switch {
    position: relative;
    display: inline-block;
    width: 60px;
    height: 34px;
  }

  /* Hide default HTML checkbox */
  .switch input {
    opacity: 0;
    width: 0;
    height: 0;
  }

  /* The slider */
  .slider {
    position: absolute;
    cursor: pointer;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background-color: #ccc;
    -webkit-transition: .4s;
    transition: .4s;
  }

  .slider:before {
    position: absolute;
    content: "";
    height: 26px;
    width: 26px;
    left: 4px;
    bottom: 4px;
    background-color: white;
    -webkit-transition: .4s;
    transition: .4s;
  }

  input:checked+.slider {
    background-color: #2196F3;
  }

  input:focus+.slider {
    box-shadow: 0 0 1px #2196F3;
  }

  input:checked+.slider:before {
    -webkit-transform: translateX(26px);
    -ms-transform: translateX(26px);
    transform: translateX(26px);
  }

  /* Rounded sliders */
  .slider.round {
    border-radius: 34px;
  }

  .slider.round:before {
    border-radius: 50%;
  }
</style>