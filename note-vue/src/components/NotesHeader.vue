<template>
  <div>
    <div class="flex items-center justify-between mb-4 space-x-4">
      <h1 class="text-2xl font-bold" style="color: #34699A;">WE NOTES APP</h1>

      <div class="flex items-center space-x-4 flex-grow max-w-6xl">
        <input
          :value="search"
          @input="updateSearch"
          type="text"
          placeholder="🔍 Search notes..."
          class="px-4 py-2 border rounded-xl flex-grow"
        />

        <select
          v-model="localSortBy"
          @change="updateSort"
          class="border rounded px-2 py-2"
          aria-label="Sort notes"
        >
          <option value="title">Title (A-Z)</option>
          <option value="createdAt">Created Date (Newest)</option>
        </select>
      </div>

      <div class="flex items-center space-x-2 flex-shrink-0">
        <img
          src="https://ui-avatars.com/api/?name=User"
          alt="User Avatar"
          class="w-10 h-10 rounded-full border"
        />
        <span class="text-gray-700 font-medium">John Doe</span>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { defineProps, defineEmits, ref, watch } from 'vue'

const props = defineProps<{ search: string; sortBy: string }>()
const emit = defineEmits(['update:search', 'update:sortBy'])

const localSortBy = ref(props.sortBy)

watch(() => props.sortBy, (val) => {
  localSortBy.value = val
})

function updateSearch(event: Event) {
  const target = event.target as HTMLInputElement
  emit('update:search', target.value)
}

function updateSort() {
  emit('update:sortBy', localSortBy.value)
}
</script>
