<template>
    <h1>
        BUS>>STOP
    </h1>
    <div v-if="stop">
        {{ stop.name }}
    </div>

    <div v-if="upcomingServices">
        <ul>
            <li v-for="(service, index) in upcomingServices" :key="index">
                {{ timePart(service.at) }} {{service.route.name}}
            </li>
        </ul>
    </div>
</template>

<script lang="ts">
    import { Options, Vue } from 'vue-class-component';
    import Api, { StopView, ServiceUpcoming } from '@/api';

    @Options({
        props: {
            msg: String
        }
    })
    export default class StopViewPage extends Vue {

        stop: StopView | null = null;
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
            return dt.substring(11, 16);
        }
    }
</script>