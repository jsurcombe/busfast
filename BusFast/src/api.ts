import http from "@/http-common";
import { AxiosResponse } from "axios";

class Api {

  getStops(q: string | null): Promise<AxiosResponse<ClusterItem[]>> {
    return http.get(`/clusters`, { params: { q: q } });
  }

  getCluster(id: string): Promise<AxiosResponse<ClusterItem>> {
    return http.get(`/clusters/${id}`);
  }

  getUpcomingServices(clusterId: string): Promise<AxiosResponse<ServiceUpcoming[]>> {
    return http.get(`/services/upcoming`, { params: { clusterId: clusterId } });
  }

  getService(id: string): Promise<AxiosResponse<ServiceItem>> {
    return http.get(`/services/${id}`);
  }

  getServiceStops(id: string): Promise<AxiosResponse<ServiceStopItem[]>> {
    return http.get(`/services/${id}/stops`);
  }

  getJourney(fromClusterId: string, toClusterId: string): Promise<AxiosResponse<Journey>> {
    return http.get(`/journey`, { params: { fromClusterId: fromClusterId, toClusterId: toClusterId } });
  }
}

export default new Api();

export type ClusterItem = {
  id: string;
  name: string;
}

export type RouteItem = {
  name: string;
}

export type ServiceUpcoming = {
  service: ServiceItem;
  stop: ClusterStopItem;
  at: string;
  bound: string;
}

export type ClusterStopItem = {
  id: number;
}

export type ServiceStopItem = {
  stopId: number;
  cluster: ClusterItem;
  time: string;
}

export type ServiceItem = {
  id: number;
  routeName: string;
}

type Step = {
  at: string;
  description: string;
}

export type Journey = {
  steps: Step[];
}