<template>
  <div
    class="note-wrapper p-4 border rounded shadow hover:shadow-lg transition cursor-pointer"
    @click="goToDetail"
  >
    <div class="flex justify-between items-start">
      <div>
        <h3 class="font-semibold text-lg">{{ note.title }}</h3>
        <p class="text-xs text-gray-500 mt-1">
          Created: {{ formattedCreatedAt }}
        </p>
      </div>
      <div class="flex space-x-2">
        <button
          @click.stop="$emit('edit', note.id)"
          aria-label="Edit note"
          class="text-blue-500 hover:text-blue-700"
        >
          ✏️
        </button>
        <button
          @click.stop="handleDelete"
          aria-label="Delete note"
          class="text-red-500 hover:text-red-700"
        >
          🗑️
        </button>
      </div>
    </div>

    <p class="text-gray-700 text-sm line-clamp-3 whitespace-pre-wrap mt-2">
      {{ note.content }}
    </p>
  </div>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router'
import api from '../api/axios'
import { computed, defineProps } from 'vue'
import { formatDate } from '../utils/dateFormatter'

const props = defineProps<{
  note: {
    id: number
    title: string
    content: string | null
    createdAt: string
    updatedAt?: string | null
  }
}>()

const router = useRouter()

function goToDetail() {
  router.push({ name: 'NoteDetail', params: { id: props.note.id } })
}

async function handleDelete() {
  const confirmDelete = confirm('Are you sure you want to delete this note?')
  if (!confirmDelete) return

  try {
    await api.delete(`/Notes/${props.note.id}`)
    window.location.reload()
  } catch (error) {
    console.error('Failed to delete note:', error)
    alert('Failed to delete note.')
  }
}

const formattedCreatedAt = computed(() => formatDate(props.note.createdAt))
</script>

<style scoped>
.note-wrapper {
  width: 100%;
  min-height: 150px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}
</style>
