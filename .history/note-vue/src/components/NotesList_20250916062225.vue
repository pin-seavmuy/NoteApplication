<template>
  <div class="p-6">
    <h1 class="text-2xl font-bold mb-4">My Notes</h1>

    <input
      v-model="search"
      type="text"
      placeholder="Search notes..."
      class="px-4 py-2 border rounded w-full mb-4"
    />

    <div v-if="loading">Loading...</div>
    <div v-else-if="filteredNotes.length === 0">No notes found.</div>

    <div v-else class="space-y-4">
      <div
        v-for="note in filteredNotes"
        :key="note.id"
        class="p-4 border rounded shadow hover:bg-gray-50 cursor-pointer"
      >
        <h2 class="font-semibold">{{ note.title }}</h2>
        <p class="text-sm text-gray-500">{{ formatDate(note.createdAt) }}</p>
        <p class="mt-2 text-gray-700">{{ note.content }}</p>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, computed, onMounted } from 'vue'
import api from '../api/axios'

interface Note {
  id: number
  title: string
  content: string | null
  createdAt: string
}

const notes = ref<Note[]>([])
const loading = ref(false)
const search = ref('')

async function fetchNotes() {
  loading.value = true
  try {
    const res = await api.get('/Notes')
    notes.value = res.data
  } catch (error) {
    console.error('Error fetching notes:', error)
  } finally {
    loading.value = false
  }
}

const filteredNotes = computed(() =>
  notes.value.filter(note =>
    note.title.toLowerCase().includes(search.value.toLowerCase())
  )
)

function formatDate(dateStr: string) {
  return new Date(dateStr).toLocaleString()
}

onMounted(fetchNotes)
</script>
