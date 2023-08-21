<template>
  <div class="container-fluid">
    <!-- SECTION hero -->
    <div class="row hero align-items-center">
      <div class="col-12 text-center">
        <button class="btn btn-danger megrim fs-2 fw-bold me-md-2">Join A Cult?</button>
        <button data-bs-toggle="modal" data-bs-target="#createCultModal" class="btn btn-danger megrim fs-2 fw-bold">Start
          A
          Cult?</button>
      </div>
    </div>

    <!-- SECTION cults -->
    <div class="row cults-section">
      <div class="col-12">
        <p class="fs-2 join ms-3 mt-2 fw-bold text-light megrim">Join A Cult</p>
      </div>

      <!-- {{ cults }} -->

      <div v-for="cult in cults" :key="cult.id" class="col-12 col-md-8 m-auto mb-4">
        <!-- {{ cult.name }} -->

        <CultCard :cult="cult" />
      </div>
    </div>
  </div>

  <ModalComponent id="createCultModal">
    <template #modalHeader>
      Create a Cult
    </template>
    <template #modalBody>
      <CultForm />
    </template>
  </ModalComponent>
</template>

<script>
import { computed, onMounted } from 'vue';
import Pop from '../utils/Pop.js';
import { cultsService } from '../services/CultsService.js'
import { AppState } from '../AppState.js';
import CultCard from '../components/CultCard.vue';
import ModalComponent from '../components/ModalComponent.vue';

export default {
  setup() {
    async function getCults() {
      try {
        await cultsService.getCults();
      }
      catch (error) {
        Pop.error(error.message);
      }
    }
    onMounted(() => {
      getCults();
    });
    return {
      cults: computed(() => AppState.cults)
    };
  },
  components: { CultCard, ModalComponent }
}
</script>

<style scoped lang="scss">
.hero {
  height: 80vh;
  background-image:
    linear-gradient(black, black),
    url(https://images.unsplash.com/photo-1487111023822-2e903e12f6f0?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=687&q=80);
  background-size: cover;
  background-blend-mode: saturation;
  background-position: center;
}

button {
  filter: none;
}

.join {
  border-top: 3px solid whitesmoke;
  width: fit-content;
}

.cults-section {
  background-color: #1A1919;
}


@media(max-width: 767px) {
  button {
    display: block;
    width: 100%;
    margin-bottom: .5em;
  }
}
</style>
