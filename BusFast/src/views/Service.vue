<template>
    <h1>
        BUS>>SERVICE
    </h1>
    <div v-if="service">
        Route {{ service.route.name }}: {{ service.route.description }}
    </div>

    <div v-if="serviceStops">
        <ul>
            <li v-for="(ss, index) in serviceStops" v-bind:class="{ highlight: highlight(ss.stopCluster) }" :key="index">
                <router-link :to="{ name: 'Cluster', params: { id: ss.stopCluster.id }}">{{ timePart(ss.time) }} {{ss.stopCluster.name}}</router-link>
            </li>
        </ul>
    </div>
</template>

<script lang="ts">
    import { Vue } from 'vue-class-component';
    import Api, { ServiceItem, ServiceStopItem, ClusterItem } from '@/api';

    export default class ServiceViewPage extends Vue {

        service: ServiceItem | null = null;
        serviceStops: ServiceStopItem[] | null = null;

        highlight(cluster: ClusterItem) {
            return this.$route.query && this.$route.query.clusterId === cluster.id.toString();
        }

        mounted() {
            const id = this.$route.params.id as string;

            Api.getService(id)
                .then(response => {
                    this.service = response.data;
                });

            Api.getServiceStops(id)
                .then(response => {
                    this.serviceStops = response.data;
                });
        }

        timePart(dt: string): string {
            return dt.substr(0, 5);
        }
    }
</script>