<template>
    <h1>BUS>>JOURNEY</h1>

    <location placeholder="leaving from" @onchange="changedFrom(value)" @input="e => fromStop = e"></location>
    >>
    <location placeholder="going to" @onchange="changedTo(value)" @input="e => toStop = e"></location>

    <div>
        <button @click="go">go</button>
    </div>
</template>

<script lang="ts">
    import { Vue } from 'vue-class-component';
    import { ClusterItem } from '../api';

    export default class Journey extends Vue {

        mounted() {
        }

        fromStop: ClusterItem | null = null;
        toStop: ClusterItem | null = null;

        go() {

        }
    }
</script>
