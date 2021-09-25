<template>
    <div>
        <h1 style="vertical-align: middle">
            BUS>>FAST
        </h1>
        <input v-model="stopQ" @input="changedQ($event.target.value)" placeholder="find a stop">
        <ul>
            <li v-for="stop in stops" :key="stop.id">
                <router-link :to="{ name: 'Cluster', params: { id: stop.id }}">{{ stop.name }}</router-link>
            </li>
        </ul>
    </div>
</template>

<script lang="ts">
    import { Vue } from 'vue-class-component';
    import Api, { ClusterItem } from '@/api';

    export default class Home extends Vue {

        stopQ: string = '';

        unbind: (() => void) | null = null;

        stops: ClusterItem[] | null = null;

        changedQ(q: string) {
            this.$router.push(`/?q=${q}`);
        }

        mounted() {
            const update = (q: string) => {
                this.stopQ = q;
                this.getStops();
            };

            update(this.$route.query.q as string);

            this.unbind = this.$router.beforeEach((to, from, next) => {
                update(to.query.q as string);
                next();
            });
        }

        unmounted() {
            if (this.unbind)
                this.unbind();
        }

        getStops() {
            if (this.stopQ) {
                const q = this.stopQ;
                Api.getStops(this.stopQ)
                    .then(response => {
                        if (q === this.stopQ)
                            this.stops = response.data;
                    });

            } else {
                this.stops = [];
            }
        }
    }
</script>
