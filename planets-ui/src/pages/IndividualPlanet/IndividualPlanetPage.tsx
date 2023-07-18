import { useLoaderData } from "react-router-dom";
import { IndividualPlanetPageDataLoaderResponse } from "./individualPlanetPageDataLoader";

export function IndividualPlanetPage() {
  const { planet } = useLoaderData() as IndividualPlanetPageDataLoaderResponse;

  return (
    <div>
      <h2>{planet.name}</h2>
      <img src={planet.imageUrl} />
      <p>Distance to the sun: {planet.distanceToSunDisplayValue}</p>
      <p>Diameter: {planet.diameterDisplayValue}</p>
      <p>Mass: {planet.massDisplayValue}</p>
      <p>{planet.additionalInfo}</p>
    </div>
  );
}
