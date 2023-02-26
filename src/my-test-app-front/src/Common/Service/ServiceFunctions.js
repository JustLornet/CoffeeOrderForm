/**
 * Метод, который на вход принимает объект, внутри которого набор из массивов
 * и проверяет, что хотя бы один из них пуст - false, Иначе - true
 */
export const checkIfSelectionsIsIncomplete = (selections) => {
  let isAtLeastOneIsEmpty = false;
  const keys = Object.keys(selections);
  keys.forEach((key) => {
    if (Array.isArray(selections[key]) && selections[key].length === 0) {
      isAtLeastOneIsEmpty = true;
      
      return;
    }
  });

  return isAtLeastOneIsEmpty;
};
