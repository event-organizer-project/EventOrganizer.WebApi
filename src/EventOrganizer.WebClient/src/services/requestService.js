import axios from 'axios'
import { startLoading, finishLoading, setError } from '../store/generalSlice'
import store from '../store/store';

export default class RequestService {

    constructor(resourceName) {
        this.resourceName = "/api/" + resourceName;
    }

    get = (id) => {
        store.dispatch(startLoading());
        
        return axios
            .get(`${this.resourceName}/${id}`)
            .then(response => {
                return response.data;
            })
            .catch(error => {
                console.log('Error:', error);
                store.dispatch(setError(error.response.statusText));
            })
            .finally(() => {
                store.dispatch(finishLoading());
            });
    }

    getList = (top, skip) => {
        store.dispatch(startLoading());

        return axios
            .get(`${this.resourceName}?top=${top}&skip=${skip}`)
            .then(response => {
                return response.data;
            })
            .catch(error => {
                console.log('Error:', error);
                store.dispatch(setError(error.response.statusText));
                return null;
            })
            .finally(() => {
                store.dispatch(finishLoading());
            });
    }

    post(payload) {
        axios
            .post(this.resourceName, payload)
            .then(response => {
                return response.data
            })
            .catch(error => {
                console.log('Error:', error);
                store.dispatch(setError(error.response.statusText));
                return null;
            })
            .finally(() => {
                store.dispatch(finishLoading());
            });
    }

    put(payload) {
        axios
            .put(this.resourceName, payload)
            .then(response => {
                return response.data
            })
            .catch(error => {
                console.log('Error:', error);
                store.dispatch(setError(error.response.statusText));
                return null;
            })
            .finally(() => {
                store.dispatch(finishLoading());
            });
    }

    delete(id) {
        axios
            .delete(`${this.resourceName}/${id}`)
            .then(response => {
                return response.data
            })
            .catch(error => {
                console.log('Error:', error);
                store.dispatch(setError(error.response.statusText));
                return null;
            })
            .finally(() => {
                store.dispatch(finishLoading());
            });
    }
}
