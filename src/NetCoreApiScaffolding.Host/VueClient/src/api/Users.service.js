import axios from 'axios';

const url = '/api/users';
const urlAll = '/all';
const urlInitialize = '/initialize';
const urlEnableUser = '/enable';
const urlDisableUser = '/disable';

const usersService = {
  async getAll() {
    try {
      const response = await axios.get(`${url}${urlAll}`);
      return response.data;
    } catch (error) {
      throw new Error(error);
    }
  },
  initUser(request) {
    return axios.post(`${url}${urlInitialize}`, request);
  },
  enableUser(userId) {
    return axios.put(`${url}${urlEnableUser}/${userId}`);
  },
  disableUser(userId) {
    return axios.put(`${url}${urlDisableUser}/${userId}`);
  },
};

export default usersService;
