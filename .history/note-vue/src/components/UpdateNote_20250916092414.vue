<template>
  <div class="p-6 max-w-xl mx-auto">
    <h1 class="text-2xl font-bold mb-4 text-blue-600">Edit Note</h1>

    <form @submit.prevent="updateNote">
      <div class="mb-4">
        <label class="block font-medium mb-1">Title</label>
        <input
          v-model="title"
          type="text"
          class="w-full border px-3 py-2 rounded"
          required
        />
      </div>

      <div class="mb-4">
        <label class="block font-medium mb-1">Content</label>
        <textarea
          v-model="content"
          rows="5"
          class="w-full border px-3 py-2 rounded"
        ></textarea>
      </div>

      <div class="flex gap-2">
        <button type="submit" class="bg-blue-600 text-white px-4 py-2 rounded">
          Save
        </button>
        <button @click="goBack" type="button" class="border px-4 py-2 rounded">
          Cancel
        </button>
      </div>
    </form>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import api from '../api/axios'

const route = useRoute()
const router = useRouter()

const title = ref('')
const content = ref('')
const loading = ref(false)
const id = Number(route.params.id)

async function fetchNote() {
  try {
    const res = await api.get(`/Notes/${id}`)
    title.value = res.data.title
    content.value = res.data.content
  } catch (error) {
    console.error('Failed to fetch note:', error)
    alert('Note not found.')
    router.push('/')
  }
}

async function updateNote() {
  try {
    await api.put(`/Notes/${id}`, {
      title: title.value,
      content: content.value,
    })
    router.push('/')
  } catch (error) {
    console.error('Update failed:', error)
    alert('Failed to update note.')
  }
}

function goBack() {
  router.push('/')
}

onMounted(fetchNote)
</script>
