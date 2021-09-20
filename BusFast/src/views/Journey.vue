<template>
    <h1>BUS>>JOURNEY</h1>

    <location placeholder="leaving from" @onchange="changedFrom(value)"></location>
    >>
    <location placeholder="going to" @onchange="changedTo(value)"></location>

</template>

<script lang="ts">
    import { Vue } from 'vue-class-component';
    
    export default class Journey extends Vue {

        mounted() {
        }
    }
</script>
