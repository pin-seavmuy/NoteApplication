<template>
  <!-- Modal Wrapper with transparent background -->
  <div class="fixed inset-0 z-50 flex items-center justify-center pointer-events-none">
    <!-- Modal Content -->
    <div
      class="bg-white w-full max-w-xl rounded-lg shadow-lg p-6 relative pointer-events-auto"
    >
      <!-- Close Button -->
      <button
        @click="goBack"
        class="absolute top-3 right-3 text-gray-500 hover:text-gray-700 text-xl"
      >
        &times;
      </button>

      <div v-if="loading">Loading...</div>
      <div v-else>
        <h1 class="text-2xl font-bold mb-4">Edit Note</h1>

        <form @submit.prevent="save" class="space-y-4">
          <!-- Title Input -->
          <div>
            <label class="block mb-1 font-semibold">Title</label>
            <input
              v-model="title"
              type="text"
              class="w-full border px-3 py-2 rounded focus:outline-none focus:ring-2 focus:ring-blue-400"
              required
            />
          </div>

          <!-- Content Input -->
          <div>
            <label class="block mb-1 font-semibold">Content</label>
            <textarea
              v-model="content"
              class="w-full border px-3 py-2 rounded resize-none focus:outline-none focus:ring-2 focus:ring-blue-400"
              rows="5"
            ></textarea>
          </div>

          <!-- Buttons -->
          <div class="flex space-x-3 justify-end">
            <button
              type="button"
              @click="goBack"
              class="px-4 py-2 border rounded text-gray-700 hover:bg-gray-100"
            >
              Cancel
            </button>
            <button
              type="submit"
              class="px-6 py-2 bg-blue-600 text-white rounded hover:bg-blue-700"
            >
              Save
            </button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import api from '../api/axios'

interface Note {
  id: number
  title: string
  content: string | null
  createdAt: string
  updatedAt?: string | null
}

const route = useRoute()
const router = useRouter()

const id = ref<string | number>(route.params.id || '')
const title = ref('')
const content = ref('')
const loading = ref(false)

async function loadNote() {
  loading.value = true
  try {
    const res = await api.get(`/Notes/${id.value}`)
    title.value = res.data.title
    content.value = res.data.content
  } catch (error) {
    console.error('Could not load note for editing:', error)
    alert('Note not found')
    router.push('/')
  } finally {
    loading.value = false
  }
}

async function save() {
  try {
    await api.put(`/Notes/${id.value}`, {
      title: title.value,
      content: content.value,
    })
    router.push(`/notes/${id.value}`)
  } catch (error) {
    console.error('Failed to update:', error)
    alert('Failed to update note')
  }
}

function goBack() {
  router.back()
}

onMounted(loadNote)
</script>
