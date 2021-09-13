<template>
    <h1>
        BUS>>SERVICE
    </h1>
    <div v-if="service">
        {{ service.name }}
    </div>

    <div v-if="serviceStops">
        <ul>
            <li v-for="(ss, index) in serviceStops" :key="index">
                <router-link :to="{ name: 'Stop', params: { id: ss.stop.id }}">{{ timePart(ss.time) }} {{ss.stop.name}}</router-link>
            </li>
        </ul>
    </div>
</template>

<script lang="ts">
    import { Options, Vue } from 'vue-class-component';
    import Api, { ServiceItem, ServiceStopItem } from '@/api';

    @Options({
        props: {
            msg: String
        }
    })
    export default class ServiceViewPage extends Vue {

        service: ServiceItem | null = null;
        serviceStops: ServiceStopItem[] | null = null;

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