import { useLoaderData } from "react-router-dom";
import { IndividualPlanetPageDataLoaderResponse } from "./individualPlanetPageDataLoader";

export function IndividualPlanetPage() {
  const { planet } = useLoaderData() as IndividualPlanetPageDataLoaderResponse;

  return (
    <div className="text-center flex flex-col">
      <img className="grow basis-0 object-cover" src={planet.imageUrl} />
      <div className="grow basis-0 flex flex-col justify-center gap-y-2">
        <h2 className="text-3xl font-medium mb-2">{planet.name}</h2>
        <p>Distance to the sun: {planet.distanceToSunDisplayValue}</p>
        <p>Diameter: {planet.diameterDisplayValue}</p>
        <p>Mass: {planet.massDisplayValue}</p>
        <p>{planet.additionalInfo}</p>
      </div>
    </div>
  );
}
