<template>
  <div class="min-h-screen bg-gray-50">
    <!-- Header with search -->
    <Header />

    <div class="p-6">
      <h1 class="text-2xl font-bold mb-4">My Notes</h1>

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
