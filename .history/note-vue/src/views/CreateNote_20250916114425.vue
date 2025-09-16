<template>
  <div class="p-6 max-w-md mx-auto">
   
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import api from '../api/axios'

const title = ref('')
const content = ref('')
const loading = ref(false)
const error = ref<string | null>(null)
const success = ref(false)

const router = useRouter()

async function handleSubmit() {
  if (!title.value.trim()) {
    error.value = 'Title is required.'
    return
  }
  loading.value = true
  error.value = null
  success.value = false

  try {
    await api.post('/Notes', {
      title: title.value,
      content: content.value || null,
    })
    success.value = true
    // Optionally, reset form
    title.value = ''
    content.value = ''
    
    // Navigate back to notes list after 1 second delay
    setTimeout(() => {
      router.push('/')
    }, 1000)
  } catch (err) {
    error.value = 'Failed to create note. Please try again.'
    console.error(err)
  } finally {
    loading.value = false
  }
}
</script>
