<!-- src/pages/NoteDetail.vue -->
<template>
  <div class="p-6">
    <router-link to="/" class="text-blue-600 hover:underline mb-4 block">← Back to Notes</router-link>

    <div v-if="loading">Loading...</div>
    <div v-else-if="!note">Note not found.</div>
    <div v-else class="space-y-4">
      <h1 class="text-2xl font-bold">{{ note.title }}</h1>
      <p class="text-sm text-gray-500">Created: {{ formatDate(note.createdAt) }}</p>
      <p v-if="note.updatedAt" class="text-sm text-gray-500">Updated: {{ formatDate(note.updatedAt) }}</p>
      <div class="mt-4 text-gray-800 whitespace-pre-wrap">{{ note.content }}</div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import api from '../api/axios'

interface Note {
  id: number
  title: string
  content: string | null
  createdAt: string
  updatedAt: string | null
}

const route = useRoute()
const note = ref<Note | null>(null)
const loading = ref(false)

async function fetchNote() {
  const id = route.params.id
  loading.value = true
  try {
    const res = await api.get(`/Notes/${id}`)
    note.value = res.data
  } catch (err) {
    console.error('Failed to fetch note:', err)
    note.value = null
  } finally {
    loading.value = false
  }
}

function formatDate(dateStr: string) {
  return new Date(dateStr).toLocaleString()
}

onMounted(fetchNote)
</script>
