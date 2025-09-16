<template>
  <div class="p-6 max-w-xl mx-auto">
    <div v-if="loading">Loading...</div>
    <div v-else>
      <h1 class="text-2xl font-bold mb-4">Edit Note</h1>
      <form @submit.prevent="save">
        <div class="mb-4">
          <label class="block mb-1">Title</label>
          <input v-model="title" type="text" class="w-full border px-3 py-2 rounded" required />
        </div>
        <div class="mb-4">
          <label class="block mb-1">Content</label>
          <textarea v-model="content" class="w-full border px-3 py-2 rounded" rows="5"></textarea>
        </div>
        <div class="flex space-x-2">
          <button type="submit" class="bg-blue-600 text-white px-4 py-2 rounded">Save</button>
          <button type="button" @click="goBack" class="border px-4 py-2 rounded">Cancel</button>
        </div>
      </form>
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
    router.push(`/notes/${id.value}`)  // go to detail page after saving
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
