<template>
    <h1>BUS>>FAST</h1>
    <input v-model="stopQ" v-on:keyup="getStops()" placeholder="find a stop">
    <ul>
        <li v-for="stop in stops" :key="stop.id">
            {{ stop.name }}
        </li>
    </ul>
</template>

<script lang="ts">
    import { Options, Vue } from 'vue-class-component';
    import Api, { Stop } from '@/api';

    @Options({
        props: {
            msg: String
        }
    })
    export default class HelloWorld extends Vue {

        stopQ!: string;

        stops: Stop[] | null = null;

        getStops() {
            Api.getStops(this.stopQ)
                .then(response => {
                    this.stops = response.data;
                })
                .catch((e: Error) => {
                    console.log(e);
                });
        }

        mounted() {
            this.getStops();
        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    h3 {
        margin: 40px 0 0;
    }

    ul {
        list-style-type: none;
        padding: 0;
    }

    a {
        color: #42b983;
    }
</style>
