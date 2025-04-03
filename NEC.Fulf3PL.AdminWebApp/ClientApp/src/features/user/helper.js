export const EMAIL_REGEX = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;

export const PASSWORD_CRITERIA = /^(?=.*?[a-zA-Z])(?=.*?[0-9]).{8,}$/;

export const NAME_CRITERIA = /[^0-9a-zA-Z ]/gm;
