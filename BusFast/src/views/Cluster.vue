<template>
    <h1>
        BUS>>STOP
    </h1>
    <div v-if="cluster">
        {{ cluster.name }}
    </div>

    <div v-if="upcomingServices">
        <ul>
            <li v-for="(ss, index) in upcomingServices" :key="index">
                <div v-bind:class="{ highlight: highlight(ss) }" >
                    <router-link :to="{ name: 'Service', params: { id: ss.service.id }, query: { stopId: ss.stop.id }}">{{ timePart(ss.at) }}: {{ss.service.routeName}} <span v-if="ss.stop.bound">{{ss.stop.bound}}</span></router-link>
                </div>
                <div>
                    {{ss.service.description}}
                </div>
            </li>
        </ul>
    </div>
</template>

<script lang="ts">
    import { Vue } from 'vue-class-component';
    import Api, { ClusterItem, ServiceUpcoming } from '@/api';

    export default class ClusterViewPage extends Vue {

        cluster: ClusterItem | null = null;
        upcomingServices: ServiceUpcoming[] | null = null;

        highlight(service: ServiceUpcoming) {
            return this.$route.query && this.$route.query.serviceId === service.service.id.toString() && this.$route.query.stopId === service.stop.id.toString();
        }

        mounted() {
            const id = this.$route.params.id as string;

            Api.getCluster(id)
                .then(response => {
                    this.cluster = response.data;
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