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
                <router-link :to="{ name: 'Service', params: { id: ss.service.id }, query: { clusterId: $route.params.id }}">{{ timePart(ss.at) }} #{{ss.service.route.name}}: {{ss.service.route.description}}</router-link>
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