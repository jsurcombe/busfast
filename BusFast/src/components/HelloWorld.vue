<template>
    <h1>BUS>>FAST</h1>
    <input v-model="stopQ" v-on:keyup="getStops()" placeholder="find a stop">
    <ul>
        <li v-for="stop in stops" :key="stop.id">
            <a :href="'stop/' + stop.id">{{ stop.name }}</a>
        </li>
    </ul>
</template>

<script lang="ts">
</script>
