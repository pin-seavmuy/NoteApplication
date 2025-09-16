<template>
  <div>
    <!-- Global Header Component -->
    <Header @search="handleSearch" />

    <!-- Notes grid -->
    <div class="p-6">
      <div v-if="loading">Loading...</div>
      <div v-else-if="filteredNotes.length === 0">No notes found.</div>

      <div
        v-else
        class="grid gap-4 grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4"
      >
        <NoteWrapper
          v-for="note in filteredNotes"
          :key="note.id"
          :note="note"
        />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import api from '../api/axios'
import NoteWrapper from '../components/NoteWrapper.vue'
import Header from '../components/Header.vue'

interface Note {
  id: number
  title: string
  content: string | null
  createdAt: string
  updatedAt?: string | null
}

const notes = ref<Note[]>([])
const loading = ref(false)
const search = ref('')

// Fetch notes from API
async function fetchNotes() {
  loading.value = true
  try {
    const res = await api.get('/Notes')
    notes.value = res.data as Note[]
  } catch (error) {
    console.error('Error fetching notes:', error)
  } finally {
    loading.value = false
  }
}

// Computed: filter notes based on search
const filteredNotes = computed(() =>
  notes.value.filter(note =>
    note.title.toLowerCase().includes(search.value.toLowerCase())
  )
)

// Handle search input from Header component
function handleSearch(e: string) {
  search.value = e
}

onMounted(fetchNotes)
</script>
