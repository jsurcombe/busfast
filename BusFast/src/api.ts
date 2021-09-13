import http from "@/http-common";
import { AxiosResponse } from "axios";

class Api {

  getStops(q: string): Promise<AxiosResponse<StopItem[]>> {
    return http.get(`/stops`, { params: { q: q } });
  }

  getStop(id: number): Promise<AxiosResponse<StopItem>> {
    return http.get(`/stops/${id}`);
  }

  getUpcomingServices(id: number): Promise<AxiosResponse<ServiceUpcoming[]>> {
    return http.get(`/services/upcoming`, { params: { stopId: id } });
  }

  getService(id: string): Promise<AxiosResponse<ServiceItem>> {
    return http.get(`/services/${id}`);
  }

  getServiceStops(id: string): Promise<AxiosResponse<ServiceStopItem[]>> {
    return http.get(`/services/${id}/stops`);
  }
}

export default new Api();

export type StopItem = {
  id: number;
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
  stop: StopItem;
  time: string;
}

export type ServiceItem = {
  id: number;
  routeName: string;
}

