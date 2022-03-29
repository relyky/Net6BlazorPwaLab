
function Bar(_name)
{
  const name = _name;

  const toString = () => {
    return `I am Bar:${name}.`;
  }

  return {
    name,
    toString
  };
}

export { Bar };
