export const ApiConstant = {
  auth: {
    login: 'Accounts/signIn',
    register: 'Accounts/signUp',
    changePassword: 'Accounts/changePassword'
  },
  post: {
    postAll: 'Posts/getAll',
    createPost: 'Posts/create',
    postById: 'Posts/getById'
  },
  user: {
    currentUser: 'Users/getLoginUser',
    changeAvatar: 'Users/avatar',
    changeInfor: 'Users/profile',
    deleteAccount: 'Users/delete',
    getUserById: 'Users/getById',
    followUser: 'Follow/follower',
    unFollowUser: 'Follow/unfollower',
    checkFollow: 'Follow/checkFollower'
  },
  collection: {
    collectionAll: 'Posts/getAll'
  }
};
