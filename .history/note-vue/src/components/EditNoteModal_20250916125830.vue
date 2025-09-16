<template>
  <div
    v-if="show"
    class="fixed inset-0 flex items-center justify-center z-50"
    @click.self="close"
    :style="{ backgroundColor: 'rgba(0, 0, 0, 0.4)' }"
  >
    <div class="rounded-lg shadow-lg p-6 w-full max-w-md relative bg-white">
      <button
        @click="close"
        class="absolute top-3 right-3 text-gray-500 hover:text-gray-800 text-xl font-bold"
        aria-label="Close modal"
      >
        &times;
      </button>

      <h2 class="text-2xl font-bold mb-6">Edit Note</h2>

      <form @submit.prevent="save" class="space-y-4">
        <div>
          <label for="title" class="block mb-1 font-semibold">Title</label>
          <input
            id="title"
            v-model="title"
            type="text"
            required
            placeholder="Note title"
            class="w-full px-4 py-2 border rounded"
          />
        </div>

        <div>
          <label for="content" class="block mb-1 font-semibold">Content</label>
          <textarea
            id="content"
            v-model="content"
            rows="5"
            placeholder="Edit your note here..."
            class="w-full px-4 py-2 border rounded resize-none"
          ></textarea>
        </div>

        <div class="flex space-x-2">
          <button
            type="submit"
            :disabled="loading"
            class="bg-blue-600 text-white px-6 py-2 rounded hover:bg-blue-700 disabled:opacity-50"
          >
            {{ loading ? 'Saving...' : 'Save Changes' }}
          </button>
          <button
            type="button"
            @click="close"
            class="px-6 py-2 rounded border border-gray-400 hover:bg-gray-100"
          >
            Cancel
          </button>
        </div>
      </form>

      <p v-if="error" class="mt-4 text-red-600">{{ error }}</p>
      <p v-if="success" class="mt-4 text-green-600">Note updated successfully!</p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, watch, defineProps, defineEmits } from 'vue'
import api from '../api/axios'

const props = defineProps<{
  show: boolean
  noteId: number | string | null
}>()

const emit = defineEmits(['update:show', 'noteUpdated'])

const title = ref('')
const content = ref('')
const loading = ref(false)
const error = ref<string | null>(null)
const success = ref(false)

watch(() => props.show, async (visible) => {
  if (visible && props.noteId) {
    // Reset state
    loading.value = true
    title.value = ''
    content.value = ''
    error.value = null
    success.value = false

    try {
      const res = await api.get(`/Notes/${props.noteId}`)
      title.value = res.data.title
      content.value = res.data.content
    } catch (err) {
      console.error('Failed to fetch note:', err)
      error.value = 'Note not found.'
    } finally {
      loading.value = false
    }
  }
})

function close() {
  emit('update:show', false)
}

async function save() {
  if (!title.value.trim()) {
    error.value = 'Title is required.'
    return
  }

  loading.value = true
  error.value = null
  success.value = false

  try {
    await api.put(`/Notes/${props.noteId}`, {
      title: title.value,
      content: content.value,
    })
    success.value = true
    emit('noteUpdated')
    setTimeout(() => {
      close()
    }, 1000)
  } catch (err) {
    console.error('Failed to update note:', err)
    error.value = 'Failed to update note. Please try again.'
  } finally {
    loading.value = false
  }
}
</script>
