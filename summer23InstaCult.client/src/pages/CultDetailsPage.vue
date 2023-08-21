<template>
  <div v-if="cult" class="container-fluid">
    <div class="row cult-details align-items-center">
      <div class="col-12 ps-5">
        <h1>{{ cult.name }}</h1>
        <button class="btn btn-outline-light">
          Join
        </button>
      </div>
    </div>
    <div class="row">
      <div class="col-12 col-md-6">
        {{ cult.description }}
      </div>
      <div class="col-12 col-md-6">
        <h2>{{ cult.leader.name }}</h2>
        <img :src="cult.leader.picture" :alt="cult.leader.name" class="rounded-circle img-fluid">
      </div>
    </div>
  </div>

  <!-- SECTION placeholder -->
  <div v-else class="container-fluid">
    <div class="row">
      <div class="col-12 p-4">
        <p class="fs-1">
          Loading....
        </p>
      </div>
    </div>
  </div>
</template>


<script>
import { useRoute } from 'vue-router';
import Pop from '../utils/Pop.js';
import { cultsService } from '../services/CultsService.js';
import { computed, watchEffect } from 'vue';
import { AppState } from '../AppState.js';

export default {
  setup() {
    const route = useRoute()

    async function getCultById(cultId) {
      try {
        await cultsService.getCultById(cultId)
      } catch (error) {
        Pop.error(error.message)
      }
    }

    watchEffect(() => {
      getCultById(route.params.cultId)
    })


    return {
      cult: computed(() => AppState.activeCult),
      cultCoverImg: computed(() => `url(${AppState.activeCult?.coverImg})`)
    }
  }
}
</script>


<style lang="scss" scoped>
.cult-details {
  min-height: 60vh;
  background-image: v-bind(cultCoverImg);
  background-size: cover;
  background-position: center;
}
</style>