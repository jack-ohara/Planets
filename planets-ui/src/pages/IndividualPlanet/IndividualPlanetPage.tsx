import { useLoaderData } from "react-router-dom";

export function IndividualPlanetPage() {
  const data = useLoaderData() as { test: string };

  return (
    <div>
      Hello Venus!
      <code>{JSON.stringify(data)}</code>
    </div>
  );
}
