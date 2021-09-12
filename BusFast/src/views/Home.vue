<template>
    <div class="home">
        <img alt="Vue logo" src="../assets/logo.png">

        <h1>BUS>>FAST</h1>
        <input v-model="stopQ" v-on:keyup="changedQ()" placeholder="find a stop">
        <ul>
            <li v-for="stop in stops" :key="stop.id">
                <router-link :to="{ name: 'Stop', params: { id: stop.id }}">{{ stop.name }}</router-link>
            </li>
        </ul>
    </div>
</template>

<script lang="ts">
    import { Options, Vue } from 'vue-class-component';
    import Api, { Stop } from '@/api';

    @Options({
        props: {
            msg: String
        }
    })
    export default class Home extends Vue {

        stopQ: string = '';

        stops: Stop[] | null = null;

        changedQ() {
            this.$router.push(`/?q=${this.stopQ}`);
            this.getStops();
        }

        mounted() {
            this.stopQ = this.$route.query.q as string;
            if (this.stopQ) {
                this.getStops();
            }
        }

        getStops() {
            Api.getStops(this.stopQ)
                .then(response => {
                    this.stops = response.data;
                });
        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    h3 {
        margin: 40px 0 0;
    }

    a {
        color: #42b983;
    }
</style>
