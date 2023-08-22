<template>
  <div v-if="cult" class="container-fluid">
    <div class="row cult-details align-items-center">
      <div class="col-12 ps-5">
        <h1>{{ cult.name }}</h1>
        <button v-if="!foundCultMember && account.id" @click="joinCult()" class="btn btn-light">
          Join
        </button>
      </div>
    </div>
    <div class="row">
      <div class="col-12 col-md-6">
        {{ cult.description }}
      </div>
      <div class="col-12 col-md-6">
        <div class="mb-3">
          <h2>Leader: {{ cult.leader.name }}</h2>
          <img :src="cult.leader.picture" :alt="cult.leader.name" class="rounded-circle img-fluid">
        </div>

        <div class="row mb-3">
          <div v-for="cultist in cultists" :key="cultist.id" class="col-4 position-relative">
            <img :title="cultist.name" :src="cultist.picture" :alt="cultist.name"
              class="img-fluid rounded-circle cultist-picture">
            <i @click="removeCultist(cultist.cultMemberId)" v-if="cult.leaderId == account.id"
              title="Send the cultist on a 'special' mission" class="mdi mdi-axe fs-2 selectable" role="button"></i>
          </div>
        </div>
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
import { cultMembersService } from '../services/CultMembersService.js'
import { logger } from '../utils/Logger.js';

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

    async function getCultistsByCultId(cultId) {
      try {
        await cultMembersService.getCultistsByCultId(cultId)
      } catch (error) {
        Pop.error(error.message)
      }
    }

    watchEffect(() => {
      getCultById(route.params.cultId)
      getCultistsByCultId(route.params.cultId)
    })


    return {
      cult: computed(() => AppState.activeCult),
      cultists: computed(() => AppState.cultists),
      cultCoverImg: computed(() => `url(${AppState.activeCult?.coverImg})`),
      account: computed(() => AppState.account),
      foundCultMember: computed(() => AppState.cultists.find(cultist => cultist.id == AppState.account?.id)),

      async joinCult() {
        try {
          const cultId = route.params.cultId
          await cultMembersService.joinCult(cultId)
        } catch (error) {
          Pop.error(error.message)
        }
      },
      async removeCultist(cultMemberId) {
        try {
          // logger.log(cultMemberId)
          const wantsToRemove = await Pop.confirm('Are you sure you want to remove this cultist?')

          if (!wantsToRemove) { return }

          await cultMembersService.removeCultist(cultMemberId)
        } catch (error) {
          Pop.error(error.message)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
h1 {
  text-shadow: 1px 1px 10px white;
}

.cult-details {
  min-height: 60vh;
  background-image: v-bind(cultCoverImg);
  background-size: cover;
  background-position: center;
}

.cultist-picture {
  height: 12vh;
  width: 12vh;
}

.mdi-axe {
  position: absolute;
  left: 43%;
  top: 3%;
}
</style>