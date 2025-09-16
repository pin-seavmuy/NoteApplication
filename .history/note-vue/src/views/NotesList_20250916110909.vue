<template>
  <div class="p-6">
    <div class="flex items-center justify-between mb-4 space-x-72">
      <h1 class="text-2xl font-bold flex-shrink-0">WE NOTES</h1>

      <input
        v-model="search"
        type="text"
        placeholder="🔍 Search notes..."
        class="px-4 py-2 border rounded-xl flex-grow"
      />

      <div class="flex items-center space-x-2 flex-shrink-0">
        <img
          src="https://ui-avatars.com/api/?name=User"
          alt="User Avatar"
          class="w-10 h-10 rounded-full border"
        />
        <span class="text-gray-700 font-medium">John Doe</span>
      </div>
    </div>

    <div class="flex items-center justify-between mb-4">
      <h1 class="text-2xl font-bold">My Notes</h1>
      <router-link
        to="/create"
        class="text-base px-4 py-2 rounded text-black hover:brightness-90"
        style="background-color: #93DA97;"
      >
        ➕  Create Note
      </router-link>

    </div>



    <input
      v-model="search"
      type="text"
      placeholder="Search notes..."
      class="px-4 py-2 border rounded w-full mb-4"
    />

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
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import api from '../api/axios'
import NoteWrapper from '../components/NoteWrapper.vue'

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
