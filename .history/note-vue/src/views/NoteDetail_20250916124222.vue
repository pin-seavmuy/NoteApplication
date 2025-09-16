<template>
  <div class="p-4 max-w mx-auto">
    <!-- Back Button -->
    <button
      @click="goBack"
      class="mb-4 text-md text-blue-600 hover:underline flex items-center"
    >
      ← Back
    </button>

    <!-- Note Content -->
    <div v-if="loading">Loading...</div>
    <div v-else-if="!note">Note not found.</div>
    <div v-else>
      <h1 class="text-xl font-bold mb-4">⭐ {{ note.title }}</h1>
      <p class="text-sm text-gray-500">Created: {{ formatDate(note.createdAt) }}</p>
      <p v-if="note.updatedAt" class="text-sm text-gray-500">Updated: {{ formatDate(note.updatedAt) }}</p>
      <div class="mt-4 text-gray-800 whitespace-pre-wrap">{{ note.content }}</div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
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
const note = ref<Note | null>(null)
const loading = ref(false)

async function fetchNote() {
  loading.value = true
  try {
    const id = route.params.id
    const res = await api.get(`/Notes/${id}`)
    note.value = res.data as Note
  } catch (error) {
    console.error('Failed to fetch note:', error)
  } finally {
    loading.value = false
  }
}

function formatDate(dateStr: string) {
  return new Date(dateStr).toLocaleString()
}

function goBack() {
  router.back()
}

onMounted(fetchNote)
</script>
