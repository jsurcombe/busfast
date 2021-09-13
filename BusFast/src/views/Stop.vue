<template>
    <h1>
        BUS>>STOP
    </h1>
    <div v-if="stop">
        {{ stop.name }}
    </div>

    <div v-if="upcomingServices">
        <ul>
            <li v-for="(ss, index) in upcomingServices" :key="index">
                <router-link :to="{ name: 'Service', params: { id: ss.service.id }, query: { stopId: $route.params.id }}">{{ timePart(ss.at) }} {{ss.service.routeName}}</router-link>
            </li>
        </ul>
    </div>
</template>

<script lang="ts">
    import { Options, Vue } from 'vue-class-component';
    import Api, { StopItem, ServiceUpcoming } from '@/api';

    @Options({
        props: {
            msg: String
        }
    })
    export default class StopViewPage extends Vue {

        stop: StopItem | null = null;
        upcomingServices: ServiceUpcoming[] | null = null;

        mounted() {
            const id = +this.$route.params.id;

            Api.getStop(id)
                .then(response => {
                    this.stop = response.data;
                });

            Api.getUpcomingServices(id)
                .then(response => {
                    this.upcomingServices = response.data;
                });
        }

        timePart(dt: string): string {
            return dt.substr(11, 5);
        }
    }
</script>