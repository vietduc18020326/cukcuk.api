import axios from 'axios'
import {apiUrl} from '../config.json'

const api = apiUrl + 'department';

const department = axios.get(api)
    .then(res => {
        return res;
    });

const getDepartment = async () => {
    let res = await department;
    return res.data;
}

export default {
    getDepartment
}