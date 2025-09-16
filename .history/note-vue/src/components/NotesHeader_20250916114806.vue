<template>
  <div>
    <div class="flex items-center justify-between mb-4 space-x-72">
      <h1 class="text-2xl font-bold" style="color: #34699A;">WE NOTES</h1>

      <input
        :value="search"
        @input="updateSearch"
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

    <!-- <div class="flex items-center justify-between mb-4">
      <router-link to="/" class="text-xl font-bold flex-shrink-0 hover:underline">All Notes</router-link>
      <router-link
        to="/create"
        class="text-base px-4 py-2 rounded text-black hover:brightness-90"
        style="background-color: #93DA97;"
      >
        ➕ Create Note
      </router-link>
    </div> -->
  </div>
</template>

<script setup lang="ts">
import { defineProps, defineEmits } from 'vue'

const props = defineProps<{ search: string }>()
const emit = defineEmits(['update:search'])

function updateSearch(event: Event) {
  const target = event.target as HTMLInputElement
  emit('update:search', target.value)
}
</script>
