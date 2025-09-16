<template>
  <div class="p-6">
    <nav></nav>

    <NotesHeader v-model:search="search" />

    <div class="flex items-center justify-between mb-4">
      <router-link to="/" class="text-xl font-bold flex-shrink-0 hover:underline">All Notes</router-link>

      <!-- Button to open modal -->
      <!-- <button
        @click="showCreateModal = true"
        class="text-base px-4 py-2 rounded text-black hover:brightness-90"
        style="background-color: #93DA97;"
      >
        ➕ Create Note
      </button> -->
    </div>

    <div v-if="loading">Loading...</div>
    <div v-else-if="filteredNotes.length === 0">No notes found.</div>

    <div
      v-else
      class="grid gap-4 grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4"
    >
      <NoteWrapper
        style="background-color: #F9F6F3;"
        v-for="note in filteredNotes"
        :key="note.id"
        :note="note"
      />
    </div>

    <!-- Create Note Modal -->
    <CreateNoteModal
      v-model:show="showCreateModal"
      @noteCreated="fetchNotes"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import api from '../api/axios'
import NoteWrapper from '../components/NoteWrapper.vue'
import NotesHeader from '../components/NotesHeader.vue'
import CreateNoteModal from '../components/CreateNoteModal.vue'

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
const showCreateModal = ref(false)

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

const filteredNotes = computed(() =>
  notes.value.filter(note =>
    note.title.toLowerCase().includes(search.value.toLowerCase())
  )
)

onMounted(fetchNotes)
</script>
