<template>
  <header class="w-full px-6 py-4 bg-white shadow flex items-center justify-between">
    <!-- Left: App Name -->
    <div class="text-xl font-bold text-blue-600">
      NoteApp
    </div>

    <!-- Center: Search Box -->
    <div class="flex-1 mx-6">
      <input
        v-model="search"
        type="text"
        placeholder="Search notes..."
        class="w-full px-4 py-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-400"
        @input="emitSearch"
      />
    </div>

    <!-- Right: Profile/User -->
    <div class="flex items-center space-x-2">
      <img
        src="https://ui-avatars.com/api/?name=User"
        alt="User Avatar"
        class="w-10 h-10 rounded-full border"
      />
      <span class="text-gray-700 font-medium">John Doe</span>
    </div>
  </header>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'

// Local search input value
const search = ref('')

// Emit search to parent
function emitSearch() {
  const event = new CustomEvent('search', { detail: search.value })
  window.dispatchEvent(event)
}
</script>
