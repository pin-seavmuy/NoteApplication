<template>
  <div class="p-6 max-w-xl mx-auto">
    <h1 class="text-2xl font-bold mb-4">Create Note</h1>

    <form @submit.prevent="createNote" class="space-y-4">
      <input
        v-model="form.title"
        type="text"
        placeholder="Title"
        class="w-full border rounded px-4 py-2"
        required
      />
      <textarea
        v-model="form.content"
        placeholder="Content"
        class="w-full border rounded px-4 py-2"
        rows="5"
      ></textarea>
      <button
        type="submit"
        class="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
      >
        Create
      </button>
    </form>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import api from '../api/axios'

const form = ref({
  title: '',
  content: '',
})

const router = useRouter()

async function createNote() {
  try {
    await api.post('/Notes', form.value)
    router.push('/') // Redirect to home or notes list page
  } catch (error) {
    console.error('Failed to create note:', error)
  }
}
</script>
