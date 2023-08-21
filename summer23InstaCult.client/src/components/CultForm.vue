<template>
  <form @submit.prevent="createCult()">
    <div class="form-floating mb-3">
      <input v-model="editable.name" type="text" class="form-control" id="cultName" placeholder="Cult Name..."
        minlength="1" maxlength="255" required>
      <label for="cultName">Cult Name</label>
    </div>
    <div class="form-floating mb-3">
      <input v-model="editable.fee" type="number" max="2147483647" min="-2147483647" class="form-control" id="cultFee"
        required placeholder="Cult Fee...">
      <label for="cultFee">Cult Fee</label>
    </div>
    <div class="form-floating mb-3">
      <input v-model="editable.coverImg" type="url" class="form-control" id="cultCoverImg" placeholder="Cult CoverImg..."
        minlength="1" maxlength="500" required>
      <label for="cultCoverImg">Cult CoverImg</label>
    </div>
    <div class="form-floating mb-3">
      <textarea v-model="editable.description" class="form-control" placeholder="Leave a description here"
        id="cultDescription" maxlength="65535" minlength="5" required></textarea>
      <label for="cultDescription">Cult Description</label>
    </div>
    <div class="text-end">
      <button class="btn btn-danger" type="submit">Submit</button>
    </div>

  </form>
</template>


<script>
import { ref } from 'vue';
import Pop from '../utils/Pop.js';
import { cultsService } from '../services/CultsService.js';
import { Modal } from 'bootstrap';

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createCult() {
        try {
          const cultData = editable.value
          await cultsService.createCult(cultData)
          editable.value = {}
          Modal.getOrCreateInstance('#createCultModal').hide()
        } catch (error) {
          Pop.error(error.message)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped></style>