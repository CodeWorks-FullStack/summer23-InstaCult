<template>
  <div class="cult-card text-light">
    <div class="p-4 d-block d-md-flex align-items-center">
      <img :src="cult.leader.picture" :alt="cult.leader.name"
        class="rounded-circle me-3 leader-picture d-none d-md-block">
      <p class="fs-2 megrim">{{ cult.leader.name }}</p>
    </div>
    <img :src="cult.coverImg" :alt="cult.name" class="cult-img">
    <div class="p-4 mb-3">

      <p class="fs-3 megrim">{{ cult.name }}</p>
      <p> {{ cult.description }}</p>
    </div>
    <div class="text-end p-4">
      <router-link :to="{ name: 'CultDetails', params: { cultId: cult.id } }">
        <button class="btn btn-danger">See more</button>
      </router-link>
    </div>
  </div>
</template>


<script>
import { Cult } from '../models/Cult.js';

export default {
  props: {
    cult: { type: Cult, required: true }
  },
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
p {
  margin-bottom: 0;
}

.cult-card {
  border: 3px solid red;
  border-radius: 15px;
  box-shadow: 3px 3px 10px red;
  background-color: #3d3d3d;
}

.leader-picture {
  height: 10vh;
  width: 10vh;
  filter: grayscale(1);

}

.cult-img {
  height: 40vh;
  width: 100%;
  object-fit: cover;
  filter: grayscale(1);
}
</style>