import http from "@/http-common";
import { AxiosResponse } from "axios";

class Api {

  getStops(q: string): Promise<AxiosResponse<ClusterItem[]>> {
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
  at: string;
}

export type ServiceStopItem = {
  stopCluster: ClusterItem;
  time: string;
}

export type ServiceItem = {
  id: number;
  routeName: string;
}

