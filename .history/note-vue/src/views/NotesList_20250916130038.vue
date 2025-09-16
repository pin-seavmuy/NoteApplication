<template>
  <div class="p-6">
    <!-- Header with App Name, Search, and User Info -->
    <NotesHeader v-model:search="search" />

    <!-- Subheader with All Notes & Create Button -->
    <div class="flex items-center justify-between my-6">
      <router-link
        to="/"
        class="text-xl font-semibold hover:underline text-gray-800"
      >
        🗒️ All Notes
      </router-link>

      <button
        @click="showCreateModal = true"
        class="px-4 py-2 rounded text-black hover:brightness-95"
        style="background-color: #93DA97;"
      >
        ➕ Create Note
      </button>
    </div>

    <!-- Loading / Empty / Notes Grid -->
    <div v-if="loading" class="text-center text-gray-500">Loading...</div>
    <div v-else-if="filteredNotes.length === 0" class="text-center text-gray-500">
      No notes found.
    </div>

    <div
      v-else
      class="grid gap-4 grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4"
    >
      <NoteWrapper
        v-for="note in filteredNotes"
        :key="note.id"
        :note="note"
        style="background-color: #F9F6F3;"
        @edit="openEditModal(note.id)"
      />
    </div>

    <!-- Create Note Modal -->
    <CreateNoteModal
      v-model:show="showCreateModal"
      @noteCreated="fetchNotes"
    />

    <!-- Edit Note Modal -->
    <EditNoteModal
      v-model:show="showEditModal"
      :noteId="selectedNoteId"
      @noteUpdated="fetchNotes"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import api from '../api/axios'

// Components
import NoteWrapper from '../components/NoteWrapper.vue'
import NotesHeader from '../components/NotesHeader.vue'
import CreateNoteModal from '../components/CreateNoteModal.vue'
import EditNoteModal from '../components/EditNoteModal.vue'

// Types
interface Note {
  id: number
  title: string
  content: string | null
  createdAt: string
  updatedAt?: string | null
}

// State
const notes = ref<Note[]>([])
const loading = ref(false)
const search = ref('')
const showCreateModal = ref(false)
const showEditModal = ref(false)
const selectedNoteId = ref<number | null>(null)

// Fetch Notes
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

// Filtered notes
const filteredNotes = computed(() =>
  notes.value.filter(note =>
    note.title.toLowerCase().includes(search.value.toLowerCase())
  )
)

// Open Edit Modal
function openEditModal(noteId: number) {
  selectedNoteId.value = noteId
  showEditModal.value = true
}

// On mount
onMounted(fetchNotes)
</script>
